using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEditor;

namespace Unity.QuickSearch
{
    /// <summary>
    /// An async search session tracks all incoming items found by a search provider that weren't returned right away after the search was initiated.
    /// </summary>
    class AsyncSearchSession
    {
        /// <summary>
        /// This event is used to receive any async search result.
        /// </summary>
        public event Action<IEnumerable<SearchItem>> asyncItemReceived;

        public event Action sessionStarted;
        public event Action sessionEnded;

        private const long k_MaxTimePerUpdate = 10; // milliseconds

        private StackedEnumerator<SearchItem> m_ItemsEnumerator = new StackedEnumerator<SearchItem>();
        private long m_MaxFetchTimePerProviderMs;

        /// <summary>
        /// Checks if this async search session is active.
        /// </summary>
        public bool searchInProgress { get; set; } = false;

        /// <summary>
        /// Called when the system is ready to process any new async results.
        /// </summary>
        public void OnUpdate()
        {
            var newItems = new List<SearchItem>();
            var atEnd = !FetchSome(newItems, m_MaxFetchTimePerProviderMs);

            if (newItems.Count > 0)
                asyncItemReceived?.Invoke(newItems);

            if (atEnd)
            {
                Stop();
            }
        }

        /// <summary>
        /// Hard reset an async search session.
        /// </summary>
        /// <param name="itemEnumerator">The enumerator that will yield new search results. This object can be an IEnumerator or IEnumerable</param>
        /// <param name="maxFetchTimePerProviderMs">The amount of time allowed to yield new results.</param>
        /// <remarks>Normally async search sessions are re-used per search provider.</remarks>
        public void Reset(object itemEnumerator, long maxFetchTimePerProviderMs = k_MaxTimePerUpdate)
        {
            // Remove and add the event handler in case it was already removed.
            Stop();
            searchInProgress = true;
            m_MaxFetchTimePerProviderMs = maxFetchTimePerProviderMs;
            m_ItemsEnumerator = new StackedEnumerator<SearchItem>(itemEnumerator);
            EditorApplication.update += OnUpdate;
        }

        internal void Start()
        {
            sessionStarted?.Invoke();
        }

        /// <summary>
        /// Stop the async search session and discard any new search results.
        /// </summary>
        public void Stop()
        {
            if (searchInProgress)
                sessionEnded?.Invoke();

            searchInProgress = false;
            EditorApplication.update -= OnUpdate;
            m_ItemsEnumerator.Clear();
        }

        /// <summary>
        /// Request to fetch new async search results.
        /// </summary>
        /// <param name="items">The list of items to append new results to.</param>
        /// <param name="quantity">The maximum amount of items to be added to @items</param>
        /// <param name="doNotCountNull">Ignore all yield return null results.</param>
        /// <returns>Returns true if there is still some results to fetch later or false if we've fetched everything remaining.</returns>
        public bool FetchSome(List<SearchItem> items, int quantity, bool doNotCountNull)
        {
            if (m_ItemsEnumerator.Count == 0)
                return false;

            var atEnd = false;
            for (var i = 0; i < quantity && !atEnd; ++i)
            {
                atEnd = !m_ItemsEnumerator.NextItem(out var item);
                if (item == null)
                {
                    if (doNotCountNull)
                        --i;
                    continue;
                }
                items.Add(item);
            }

            return !atEnd;
        }

        /// <summary>
        /// Request to fetch new async search results.
        /// </summary>
        /// <param name="items">The list of items to append new results to.</param>
        /// <param name="quantity">The maximum amount of items to add to @items</param>
        /// <param name="doNotCountNull">Ignore all yield return null results.</param>
        /// <param name="maxFetchTimeMs">The amount of time allowed to yield new results.</param>
        /// <returns>Returns true if there is still some results to fetch later or false if we've fetched everything remaining.</returns>
        public bool FetchSome(List<SearchItem> items, int quantity, bool doNotCountNull, long maxFetchTimeMs)
        {
            if (m_ItemsEnumerator.Count == 0)
                return false;

            var atEnd = false;
            var timeToFetch = Stopwatch.StartNew();
            for (var i = 0; i < quantity && !atEnd && timeToFetch.ElapsedMilliseconds < maxFetchTimeMs; ++i)
            {
                atEnd = !m_ItemsEnumerator.NextItem(out var item);
                if (item == null)
                {
                    if (doNotCountNull)
                        --i;
                    continue;
                }
                items.Add(item);
            }

            return !atEnd;
        }

        /// <summary>
        /// Request to fetch new async search results.
        /// </summary>
        /// <param name="items">The list of items to append new results to.</param>
        /// <param name="maxFetchTimeMs">The amount of time allowed to yield new results.</param>
        /// <returns>Returns true if there is still some results to fetch later or false if we've fetched everything remaining.</returns>
        public bool FetchSome(List<SearchItem> items, long maxFetchTimeMs)
        {
            if (m_ItemsEnumerator.Count == 0)
                return false;

            var atEnd = false;
            var timeToFetch = Stopwatch.StartNew();
            while (!atEnd && timeToFetch.ElapsedMilliseconds < maxFetchTimeMs)
            {
                atEnd = !m_ItemsEnumerator.NextItem(out var item);
                if (!atEnd && item != null)
                    items.Add(item);
            }

            return !atEnd;
        }
    }

    /// <summary>
    /// A MultiProviderAsyncSearchSession holds all the providers' async search sessions.
    /// </summary>
    class MultiProviderAsyncSearchSession
    {
        private Dictionary<string, AsyncSearchSession> m_SearchSessions = new Dictionary<string, AsyncSearchSession>();

        /// <summary>
        /// This event is used to receive any async search result.
        /// </summary>
        public event Action<IEnumerable<SearchItem>> asyncItemReceived;

        public event Action sessionStarted;
        public event Action sessionEnded;

        /// <summary>
        /// Checks if any of the providers' async search are active.
        /// </summary>
        public bool searchInProgress => m_SearchSessions.Any(session => session.Value.searchInProgress);

        /// <summary>
        /// Returns the specified provider's async search session.
        /// </summary>
        /// <param name="providerId"></param>
        /// <returns>The provider's async search session.</returns>
        public AsyncSearchSession GetProviderSession(string providerId)
        {
            if (!m_SearchSessions.TryGetValue(providerId, out var session))
            {
                session = new AsyncSearchSession();
                session.sessionStarted += OnProviderAsyncSessionStarted;
                session.sessionEnded += OnProviderAsyncSessionEnded;
                session.asyncItemReceived += OnProviderAsyncItemReceived;
                m_SearchSessions.Add(providerId, session);
            }

            return session;
        }

        private void OnProviderAsyncSessionStarted()
        {
            sessionStarted?.Invoke();
        }

        private void OnProviderAsyncSessionEnded()
        {
            sessionEnded?.Invoke();
        }

        private void OnProviderAsyncItemReceived(IEnumerable<SearchItem> obj)
        {
            asyncItemReceived?.Invoke(obj);
        }

        /// <summary>
        /// Stops all active async search sessions held by this MultiProviderAsyncSearchSession.
        /// </summary>
        public void StopAllAsyncSearchSessions()
        {
            foreach (var searchSession in m_SearchSessions)
            {
                searchSession.Value.Stop();
            }
        }

        /// <summary>
        /// Clears all async search sessions held by this MultiProviderAsyncSearchSession.
        /// </summary>
        public void Clear()
        {
            foreach (var searchSession in m_SearchSessions)
            {
                searchSession.Value.asyncItemReceived -= OnProviderAsyncItemReceived;
                searchSession.Value.sessionStarted -= OnProviderAsyncSessionStarted;
                searchSession.Value.sessionEnded -= OnProviderAsyncSessionEnded;
            }
            m_SearchSessions.Clear();
        }
    }
}
