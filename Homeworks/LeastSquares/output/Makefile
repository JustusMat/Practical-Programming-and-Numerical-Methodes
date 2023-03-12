BPATH = ../build/
DPATH = ../data/
MPATH = ../


default: Out.txt Fit.svg

Out.txt: $(addprefix $(BPATH), main.exe) $(addprefix $(DPATH), exampledata.dat)
	mono $(filter %.exe, $^) -input:$(filter %.dat, $^) > $@
	
Fit.svg: $(addprefix $(MPATH), Fit.gpi) $(addprefix $(DPATH), exampledata.dat data.data)
	gnuplot $<