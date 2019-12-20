# Bad Guys Can Dance

![screenshot](Screenshots/2019_12_20_Screenshot_00.png)

> - 2 players game made for a Ubisoft Gamejam 2019 (72 hours).
> - The theme was "Wait... Are we the bad guys?".

## Synopsis

    In a time of neon lights, fluorescent lights, and synthwave, 8 dancers are on the floor, dressed up like 80s bad guys (e.g., Darth Vador, Joker). Turns out, one of them is the actual bad guys. Someone (the hunter outside the dance floor) knows this and has his laser ready to shoot. Will he manage to kill the true bad guy without murdering innocent peoples? In the end, who is the actual bad guy...?

## Gameplay

> 8 dancers are on the floor, moving with the music beat. One of them is controlled by the dancer player. The second player (hunter player) has to find out who is the human controlled dancer and shoot him.

### Dancer player

From the dancer player prospective, you have to behave like an NPC to hide from the hunter's sight. NPCs move on the beat. They keep going forward until either the movement is impossible (e.g., Border), or the music reaches the measure (bar). The new direction is selected randomly. The goal is to remain the last survivor (in other words, let the hunter kill the maximum number of dancers before you).

### Hunter player

The hunter player has to eliminate the dancer player without killing innocent dancers (or as few as possible). At the end of the game, a recap shows the dancers status (alive or killed).

## Controls

- Dancer player: Keyboard AWSD
- Hunter player: Mouse (Left click to shoot)

## Development

- Unity 2D version 2019.2.12f1
- Wwise

## Known BUGs

- In the game recap, Skeletor is never displayed as killed, even if was killed (this appears only in the final build, not in the Unity editor).
- The laser cursor is not displayed in the sreenshots

## Screenshots

![screenshot](Screenshots/2019_12_20_Screenshot_01.png)
![screenshot](Screenshots/2019_12_20_Screenshot_02.png)
![screenshot](Screenshots/2019_12_20_Screenshot_03.png)

## Authors

- Camille Ghadban (game designer)
- Yoann Morvan (sound designer)
- Xavier Heritier (artiste)
- Constantin Masson (programmer)
