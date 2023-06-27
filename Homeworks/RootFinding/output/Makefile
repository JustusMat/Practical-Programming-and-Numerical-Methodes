BPATH = ../build/
DPATH = ../data/
MPATH = ../

default: Out.txt  BSHydrogen.svg Convergence.svg

Out.txt: $(addprefix $(BPATH), main.exe)
	mono $(filter %.exe, $^) > $@ 
	
BSHydrogen.svg: $(addprefix $(MPATH), BSHydrogen.gpi) $(addprefix $(DPATH), BSHydrogen.data)
	gnuplot $<
	
Convergence.svg: $(addprefix $(MPATH), ConvergenceTest.gpi) $(addprefix $(DPATH), accuracyConvergence.data epsilonConvergence.data rminConvergence.data rmaxConvergence.data)
	gnuplot $<