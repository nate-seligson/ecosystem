# creaturesim || July 2023
This project uses rudiementary neural networks and random number generation to create unique "organisms" that will chase after food. When the creature eats, it reproduces a child with slightly mutated genes, to create a "survival of the fittest" effect.
# Brain
The Neural Network is made up of two matrixes per layer -- One of weights, and one of biases. these are randomized during the creation, and inherited with minor changes. The inputs are given in pairs of 9: 3 R, G, and B values from each eyeball (0 if no output). Then, the matrixes are multiplied and outputted as a 3-column 1-row matrix. The program interprets whichever value is greatest for movement.
## 
index [0] = Turn Left,
index [1] = Turn Right,
index [2] = Move Forward 
# Parts
During the creation of each organism, they have a randomly chosen set of parts that can affect their chances of survival. All parts are reflected twice on either side to create symmetry.
## Eye
The eye computes 3 raycasts outwards in slighly different directions. When the raycast returns, it takes the RGB values of the SpriteRenderer component and passes those to the neural network in 9 inputs. More eyes mean more inputs for the neural network, which leads to a greater perception from the organism.
## Flipper
Adds additional speed to the creature.
## Stomach
Adds more seconds to the starvation timer of the creature.
## Spike
Kills other creatures that touch it.
## Pouch
Adds additional children upon reproduction


