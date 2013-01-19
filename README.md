Othello-riversi
===============


How to play
===========
Reversi is a strategy board game for two players, played on an 8x8 uncheckered board. There are 64 identical pieces called 'disks', which are light on one side and dark on the other—physically with an actual set, or conceptually via computer—to correspond with the opponents in a game.
Othello starts with the center 4 squares of the board occupied with 2 black and 2 white pieces arranged diagonally. True Reversi (that according to the game's original rules) starts with the board empty and the first 2 moves by each player are played into the middle 4 squares with no disk flips. Thus Reversi can start with either a parallel or diagonal layout of the first 4 pieces. Typically computer versions start as per Othello, but the name Othello (along with certain purely aesthetic features of board design) is trademarked so that use of the name Reversi is necessary to avoid legal problems. Play with the 'Reversi' opening, wherein disks of one color line up parallel to those of the other in the center 4 squares, may still be found among enthusiasts in some places. When the game is played according to the original rules, either player may force play into this Reversi opening.
Though the original rules of Reversi were such that each player was limited to using no more than half of the disks (those in possession at the start), this rule has long been out of common practice; and, if using a physical board and pieces, the player whose turn it is simply retrieves a disk that is in possession of the opponent as needed. This means that there is now only one way a player will pass (always involuntarily) rather than place a disk, while formerly there were two. In most cases a game of Reversi reaches an end in which all 64 squares are filled, but positions in which the board is not full and neither player may make a legal move also occur with some frequency.
Each player's objective is generally to have as many disks one's own color at the end as possible and for one's opponent to have as few—or, technically in consideration of the occasional game in which not all disks are placed, that the difference between the two should be as large as possible if the winner and as small as possible if the loser. However, simply winning is the basic goal, and maximizing the 'disk differential' is regarded as ancillary. (It may have more or less weight depending upon such things as tournament tiebreaks.)


This project explores Artificial Intelligence techniques in the 
board game Othello. Several Othello-playing programs were 
implemented and compared. The performance of minimax search 
algorithms, including alpha-beta, NegaScout and MTD(f), and of 
other search improvements such as transposition tables, was 
analyzed. In addition, the use of machine learning to enable 
AI players to improve play automatically through training was 
investigated.

Introduction and Background
===========================

Othello (also known as Reversi) is a two-player board game 
and abstract strategy game, like chess and checkers. I chose 
to work with Othello because it is sufficiently complex to allow 
significant exploration of advanced AI techniques, but has a 
simple set of rules compared to more complex games like 
chess. It has a moderate branching factor, larger than 
checkers and smaller than chess, for example, which makes 
advanced search techniques important without requiring a 
great deal of computational power for strong play. Although 
my AI programs are implemented to play Othello, most of the 
algorithms, data structures, and techniques I have 
investigated are designed for abstract strategy games in 
general instead of Othello specifically, and many machine 
learning algorithms are widely applicable to problems other 
than games.
The basic goal of an AI player is to consider the possible 
moves from the current game state, evaluate the position 
resulting from each move, and choose the one that appears 
best. One major component of an AI player is the static 
evaluation function, which heuristically estimates the value of 
a position without exploring moves. This value indicates 
which player has the advantage and how large that 
advantage is. A second major component is the search 
algorithm, which more accurately evaluates a state by 
looking ahead at potential moves.


Search Algorithms
=================

The minimax search algorithm is the basic algorithm to 
search the game tree. Minimax recursively evaluates a 
position by taking the best of the values for each child 
position. Alpha-beta pruning is an extremely important 
improvement to minimax that greatly reduces the number of 
nodes in the game tree that must be searched. As shown in 
the figure below, alpha-beta is far faster than minimax, 
especially at high search depth.
NegaScout and MTD(f) are enhancements of alpha-beta that 
use null-window searches, which result in many more cutoffs 
than wide window alpha-beta searches. Null-window 
searches provide only a bound on the minimax value, so 
repeated re-searches may be necessary to find the true 
value.  The chart below compares the performance of 
NegaScout, MTD(f), and alpha-beta.




