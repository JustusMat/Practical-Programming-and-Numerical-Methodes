BPATH = ../build/
DPATH = ../data/
MPATH = ../

default: Out.txt Benchmark.svg

Out.txt: $(addprefix $(BPATH), main.exe)
	$^ > $@
	
Benchmark.svg: $(addprefix $(MPATH), Benchmark.gpi) $(addprefix $(DPATH), Benchmark.data)
	gnuplot $<