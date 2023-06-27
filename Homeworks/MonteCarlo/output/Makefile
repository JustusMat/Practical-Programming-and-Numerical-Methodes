SPATH = ../source/
BPATH = ../build/
DPATH = ../data/
MPATH = ../

default: Out.txt PlainMonteCarloConvergenceTest.svg MonteCarloErrorScaling.svg

Out.txt: $(addprefix $(BPATH), main.exe)
	mono $^ > $@
	
PlainMonteCarloConvergenceTest.svg: $(addprefix $(MPATH), MonteCarloConvergenceTest.gpi) $(addprefix $(DPATH), NpointsPlain.data NpointsQuasi.data NpointsStratifiedSampling.data)
	gnuplot $<
	
MonteCarloErrorScaling.svg: $(addprefix $(MPATH), MonteCarloErrorScaling.gpi) $(addprefix $(DPATH), NpointsPlain.data NpointsQuasi.data NpointsStratifiedSampling.data)
	gnuplot $<