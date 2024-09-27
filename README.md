# Unity Learn - Object-Oriented Programming Principles

["Programming theory in action"](https://learn.unity.com/tutorial/submission-programming-theory-in-action)  
[Unity Learn profile](https://learn.unity.com/u/665f836eedbc2a2a94e96ef5?tab=profile)

## BROBBLE

Top-down arena shooter with 4 gun types. 🫧  
Survive a 1-minute wave of enemies and confront the final boss. 🦆


🎮[Play here!](https://play.unity.com/en/games/14a03810-d99d-4ae2-8bbe-2e6d6b0d7c45/brobble)  
🧑‍💻[Github project here](https://github.com/walibixo/UnityLearn_OOPPrinciples)


Examples of object-oriented principles:
-   Inheritance: **Player** and **Enemy** inherit from Unit. **Boss** from **Enemy**.
-   Abstraction: **Projectile** instantiation (with speed, life span and cooldown time) is abstracted in the **Gun** base class for all the **Gun**-derived classes.
-   Polymorphism: **Unit**-derived classes can override the **Move**/**Attack**/**Die**/etc. methods for distinct behaviors.
-   Encapsulation: **Damage (int)** in **Projectile** is read-only for all other classes.
