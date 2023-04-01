BPATH = ../build/
OPATH = ../output/

default: fitresults.data	

fitresults.data: $(addprefix $(BPATH), higgsminimisation.exe) higgs.dat 
	mono $(filter %.exe, $^) -input:$(filter %.dat, $^) > $@