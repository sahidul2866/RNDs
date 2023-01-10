import random
import sys


def tobits(s):
    result = []
    for c in s:
        bits = bin(ord(c))[2:]
        bits = bits[-3:]
        result.extend([True if b == '1' else False for b in bits])
    print(type(result), type(result[0]), sys.getsizeof(result))
    return result


f = open("input.txt", "r")
str = ''
for line in f.readlines():
    str += line
dirs = str.split(",")

f1 = open("test.txt", "w")
line = ""
for i in range(len(dirs)):
    line += dirs[i] + " "
    if i % 10000 == 0:
        f1.write(line)
        line = ""

