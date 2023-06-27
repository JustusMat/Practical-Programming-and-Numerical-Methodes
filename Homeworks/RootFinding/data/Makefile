BPATH = ../build/

default: BSHydrogen.data InvestigateConvergence
	
BSHydrogen.data: $(addprefix $(BPATH), BSHydrogen.exe)
	mono $^ > $@
	
InvestigateConvergence: $(addprefix $(BPATH), InvestigateConvergence.exe)
	mono $(filter %.exe, $^)