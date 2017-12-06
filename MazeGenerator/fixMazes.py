# pylint: disable
import random

mapa = [
[3, 5, 5, 5, 3, 3, 3, 3, 5, 5, 5, 5, 0, 0, 0, 0, 1, 1, 1, 1],
[5, 0, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 1],
[4, 0, 5, 5, 5, 0, 0, 1, 1, 1, 1, 1, 4, 1, 1, 4, 2, 0, 2, 0],
[2, 2, 2, 2, 2, 3, 3, 3, 3, 1, 4, 3, 3, 3, 3, 4, 2, 0, 1, 0],
[2, 4, 5, 5, 5, 4, 5, 5, 5, 1, 4, 2, 4, 2, 3, 4, 2, 0, 1, 0],
[2, 5, 5, 5, 5, 4, 5, 0, 0, 1, 4, 0, 0, 0, 0, 0, 0, 2, 1, 0],
[2, 3, 1, 1, 1, 4, 4, 2, 5, 1, 4, 5, 1, 3, 0, 3, 4, 2, 1, 0],
[2, 5, 0, 0, 0, 0, 0, 0, 0, 1, 2, 1, 1, 5, 0, 3, 4, 2, 1, 0],
[2, 3, 3, 3, 1, 3, 3, 3, 3, 1, 1, 3, 3, 0, 0, 0, 4, 2, 1, 1],
[2, 4, 4, 5, 3, 3, 3, 0, 0, 1, 1, 1, 4, 2, 0, 5, 4, 2, 1, 1],
[2, 2, 5, 1, 1, 1, 3, 1, 1, 1, 1, 1, 4, 2, 0, 5, 4, 2, 1, 4],
[2, 5, 2, 0, 0, 0, 3, 5, 5, 5, 5, 2, 5, 5, 5, 5, 1, 5, 5, 4],
[2, 2, 2, 2, 4, 1, 3, 1, 0, 0, 2, 2, 3, 2, 3, 5, 1, 2, 4, 4],
[2, 3, 3, 3, 3, 3, 3, 3, 3, 0, 0, 4, 0, 0, 3, 5, 1, 0, 1, 4],
[2, 4, 5, 0, 0, 0, 3, 5, 3, 0, 0, 2, 2, 2, 3, 5, 1, 2, 1, 0],
[2, 5, 2, 2, 4, 1, 3, 1, 3, 0, 0, 2, 0, 1, 1, 2, 1, 2, 2, 4],
[2, 0, 0, 0, 0, 0, 3, 0, 0, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5],
[2, 5, 5, 5, 3, 2, 3, 5, 0, 1, 3, 2, 2, 0, 0, 0, 0, 0, 4, 3],
[2, 2, 4, 4, 4, 4, 2, 4, 3, 3, 3, 1, 1, 4, 3, 1, 2, 1, 1, 1],
[3, 5, 5, 0, 2, 2, 2, 4, 2, 2, 2, 2, 2, 2, 4, 4, 4, 4, 4, 6],
]

val = 5
for i in range(len(mapa)):
    for j in range(len(mapa[i])):
        if mapa[i][j] == val:
            mapa[i][j] = random.randrange(val)
    print(mapa[i])