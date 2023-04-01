BPATH = ../build/
DPATH = ../data/
MPATH = ../

default: Out.txt  BSHydrogen.svg

Out.txt: $(addprefix $(BPATH), main.exe)
	mono $(filter %.exe, $^) > $@ 
	
BSHydrogen.svg: $(addprefix $(MPATH), BSHydrogen.gpi) $(addprefix $(DPATH), BSHydrogen.data)
	gnuplot $<