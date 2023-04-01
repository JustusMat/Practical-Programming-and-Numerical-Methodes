BPATH = ../build/
OPATH = ../output/

default:erf.data
	
erf.data: $(addprefix $(BPATH), erf.exe)
	mono $^ > $@