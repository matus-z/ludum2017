import random
from pprint import pprint
import numpy as np
import matplotlib.pyplot as plt
from matplotlib import colors


#-----------------------------------------
def array2cmap(X):
    N = X.shape[0]

    r = np.linspace(0., 1., N + 1)
    r = np.sort(np.concatenate((r, r)))[1:-1]

    rd = np.concatenate([[X[i, 0], X[i, 0]] for i in xrange(N)])
    gr = np.concatenate([[X[i, 1], X[i, 1]] for i in xrange(N)])
    bl = np.concatenate([[X[i, 2], X[i, 2]] for i in xrange(N)])

    rd = tuple([(r[i], rd[i], rd[i]) for i in xrange(2 * N)])
    gr = tuple([(r[i], gr[i], gr[i]) for i in xrange(2 * N)])
    bl = tuple([(r[i], bl[i], bl[i]) for i in xrange(2 * N)])

    cdict = {'red': rd, 'green': gr, 'blue': bl}
    return colors.LinearSegmentedColormap('my_colormap', cdict, N)


gridX = 6
gridY = 6
tileColors = 3
konec = ''

while True:
    if konec != 'n':
      gridX = int(
          raw_input('Grid horizontal size [last run {}]: '.format(gridX))
          or gridX)
      gridY = int(
          raw_input('Grid vertical size [last run {}]: '.format(gridY)) or gridY)
      tileColors = int(
          raw_input('Number of colors [last run {}]: '.format(tileColors))
          or tileColors)
    values = [[-1 for x in range(gridX)] for x in range(gridY)]
    positions = []
    for i in range(gridX):
        for j in range(gridY):
            positions.append((i, j))

    while len(positions) > 0:
        startPoint = random.choice(positions)
        # positions.remove(startPoint)
        color = random.randrange(tileColors)
        direction = random.randrange(4)
        if direction == 0:
            # UP
            length = random.randrange(startPoint[0] + 1)
            for i in range(length):
                if (startPoint[0] - i, startPoint[1]) in positions:
                    values[startPoint[0] - i][startPoint[1]] = color
                    positions.remove((startPoint[0] - i, startPoint[1]))
        elif direction == 1:
            # RIGHT
            length = random.randrange(gridX - startPoint[1])
            for i in range(length):
                if (startPoint[0], startPoint[1] + i) in positions:
                    values[startPoint[0]][startPoint[1] + i] = color
                    positions.remove((startPoint[0], startPoint[1] + i))
        elif direction == 2:
            # DOWN
            length = random.randrange(gridY - startPoint[0])
            for i in range(length):
                if (startPoint[0] + i, startPoint[1]) in positions:
                    values[startPoint[0] + i][startPoint[1]] = color
                    positions.remove((startPoint[0] + i, startPoint[1]))
        else:
            # LEFT
            length = random.randrange(startPoint[1] + 1)
            for i in range(length):
                if (startPoint[0], startPoint[1] - i) in positions:
                    values[startPoint[0]][startPoint[1] - i] = color
                    positions.remove((startPoint[0], startPoint[1] - i))

    for i in range(gridY, 0, -1):
        print(values[i - 1])

    #define the colormar
    X = np.array([
        [1., 0., 0.],  #red
        [0., 1., 0.],  #green
        [0., 0., 1.],  #blue
        [1., 1., 0.],  #yellow
        [1., 0., 1.],  #magenta
        [0., 1., 1.],  #cyan
        [0., 0., 0.]  #black
    ])
    mycmap = array2cmap(X[:tileColors])

    plt.gca().pcolormesh(values, cmap=mycmap)

    cb = plt.cm.ScalarMappable(norm=None, cmap=mycmap)
    cb.set_array(values)
    cb.set_clim((0., float(tileColors)))
    plt.gcf().colorbar(cb)
    plt.show()
    konec = raw_input("Press Enter to continue or 'q' to exit... ")
    if konec == 's':
      with open('saved.txt', 'a') as f:
        f.write('{} {} {}'.foramt(gridX, gridY, tileColors))
        for i in range(gridY, 0, -1):
            f.write('{}\n'.format(values[i - 1]))
        f.write('\n')
    if konec == 'q':
        break
