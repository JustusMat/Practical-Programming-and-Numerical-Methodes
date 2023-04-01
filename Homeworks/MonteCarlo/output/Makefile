SPATH = ../source/
BPATH = ../build/
DPATH = ../data/

default: Out.txt

Out.txt: $(addprefix $(BPATH), main.exe)
	mono $^ > $@