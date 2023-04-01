SPATH = ../source/
BPATH = ../build/
DPATH = ../data/
OPATH = ../output/
MPATH = ../

default: Out.txt erf.svg
	
Out.txt: $(addprefix $(BPATH), main.exe) $(addprefix $(SPATH), main.py)
	mono $^ >> $@ 
	@echo Being executed
	python3 $(filter %.py, $^) >> $@
	@echo executed python3
	
erf.svg: $(addprefix $(MPATH), erf.gpi) $(addprefix $(DPATH), erf.data)
	gnuplot $<
	
pythoncomparison: $(addprefix $(SPATH), main.py)
	python3 $^