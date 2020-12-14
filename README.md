# test_platformer_00

## Base info.
Auto runner. I used my own library [AssistLib](https://github.com/c1tr00z/assist-lib-git-submodule) for make this project as good as possible.

## Game Design

Game design params can be adjusted with editing scriptable objects in project

### Gameplay
Gameplay settings stores in Assets/Gameplay/Resources/GameplaySettings.asset

### SceneObjects / Placeble objects
Balance for this part can be adjusted by editing Scriptable Objects
* For adjust JumpPod please edit Assets/SceneObjects/Resources/JumpPod.asset
* For adjust Ice Pond please edit Assets/SceneObjects/Resources/IcePond.asset
* For adjust Bomb please edit Assets/SceneObjects/Resources/Bomb.asset

### Bonuses
Balance for bonuses can be adjusted with main settings asset (Assets/Bonuses/Resources/BonusesSettings.asset) and with following scriptable objects:
* For adjust Energy Restore please edit Assets/Bonuses/Resources/Bonuses/RestoreEnergy.asset
* For adjust Protection please edit Assets/Bonuses/Resources/Bonuses/Indestructable.asset
* For adjust Speed Up please edit Assets/Bonuses/Resources/Bonuses/SpeedUp.asset