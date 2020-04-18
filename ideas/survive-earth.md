# Survive Earth

You are travelers from a distant world and crash land on Earth. 
In order to survive, you must gather your colony of refugees 
and migrate from city to city while avoiding *terrible things*. 
You must decide when and where to settle, fight, or flee. 
Will you be able to overcome the odds by growing a fully established colony,
generating the parts for an escape spaceship, 
and win by evacuating from Earth?  Only time will tell;
individuals will go and come, but Earth abides!     


## Mechanics:
In this game, you crash land on Earth and your colony is scattered across the map.
You need to direct your citizens in a way that will help you escape! 
As the game starts, you see a map, and your colony is randomly distributed across the "crash zone."
After the initial setup, you can do three things.

    1. Fight for a new territory to gain ownership.
    1. Colonize a city on the map.
    1. Evacuate everyone from a city and move them to a connected owned territory.
    1. Build the escape space ship at a space port.


### Fight

As the leader of your race of mini people, you have the power to designate a fighter at any colonized territory.
You can decide to go into combat in order to gain an adjacent territory.
Gaining an adjacent territory will allow you to move there in the next round.

_I am thinking that this part can be a platformer / realtime game where you have to beat a level in order to win._


### Colonize a city

The point of colonization is to gain resources. The more resources you have, the easier it will be to escape.
In general, colonies will generate resources for the escape space ship each game round. 
More colonies will help to generate spaceship parts faster.
In a given round, you can decide how a colony should function.

    1. A colony can generate population (once per round).
    1. A colony can generate resources (generation rate is proportional to population).
    1. A colony can send 1/2 of its population to another owned city to begin colonization.
    1. A colony can evacuate (i.e. break down and stop generating to allow movement).
    1. A colony can be a designated space port to build the space ship.

Colonies that are interconnected via the map can pool resources in order to build the space ship faster.

_I am thinking that this can be done on a single map/screen where cities are graph nodes, and cities are
 connected by graph edges. Essentially, the map is just a graph data structure where you decide to colonize,
 move, or fight an adjacent node (city)._



### Escape Earth!

Once you generate enough resources, you must gather all of your refugees to the space port.
You cannot escape unless all of your population is gathered at the colony managing the space port. 
It's possible to have multiple colonies building a space port, but only one of them will launch.
You all crash-landed here on the same ship, so you must all escape on the same ship.
If you escape, you win, and your score is saved.



## Terrible things

Every round, something challenging should happen.
Here are some ideas:

    1. A blight wipes out 1/2 a colony population.
    1. Some resources go bad.
    1. Random encounter (fight!) at some node, and if you die, then you lose something.
    1. Some city-to-city connection is lost.
   



