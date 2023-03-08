BPATH = ../build/
DPATH = ../data/
MPATH = ../

default: Out.txt Hydrogen.txt Hydrogen.svg

Out.txt: $(addprefix $(BPATH), main.exe)
	$^ > $@	

Hydrogen.txt: $(addprefix $(BPATH), hydrogenswaveexample.exe)
	$^ > $@
	
Hydrogen.svg: $(addprefix $(MPATH), hydrogen.gpi)
	gnuplot $<