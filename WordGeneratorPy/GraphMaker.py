import matplotlib.pyplot as plt; plt.rcdefaults()
import numpy as np
import matplotlib.pyplot as plt

def graphMaker(graph):
    objects = graph["graphData"]["yAxis"]["data"]
    y_pos = np.arange(len(objects))
    performance = graph["graphData"]["xAxis"]["data"] #['Insignificant', 'Minor', 'Moderate', 'Major', 'Catastrophic']
    plt.bar(y_pos, performance, align='center', alpha=0.5)
    plt.xticks(y_pos, objects)
    plt.ylabel('Impact >', fontsize=22)
    plt.xlabel('\nLikelihood >', fontsize=22)
    plt.tight_layout()
    plt.savefig("graph.png")