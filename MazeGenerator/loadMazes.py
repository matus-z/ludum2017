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


def drawMap(values):
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
    f = lambda a, b: a if (a > b) else b
    tileColors = reduce(f, map(lambda x: reduce(f, x), values))
    mycmap = array2cmap(X[:tileColors + 1])

    plt.gca().pcolormesh(values, cmap=mycmap)

    cb = plt.cm.ScalarMappable(norm=None, cmap=mycmap)
    cb.set_array(values)
    cb.set_clim((0., float(tileColors)))
    plt.gcf().colorbar(cb)
    plt.show()


i = 0
j = 0
values = []
with open('saved.txt', 'r') as f:
    for line in f:
        if line[0] == '\n':
            drawMap(values)
            values = []
            continue
        elif line[0] == '[':
            line = line.strip()
            values.insert(0, eval(line))
