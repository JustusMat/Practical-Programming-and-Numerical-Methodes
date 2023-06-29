SPATH = ../source/
BPATH = ../build/
DPATH = ../data/
OPATH = ../output/
MPATH = ../

default: Out.txt Fit.svg
	
Out.txt: $(addprefix $(BPATH), main.exe)
	mono $^ > $@ 
	
Fit.svg: $(addprefix $(MPATH), Fit.gpi) $(addprefix $(DPATH), higgs.dat fitresults.data fitresultssimplex.data)
	gnuplot $<
