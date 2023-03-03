BPATH = ../build/
DPATH = ../data/
MPATH = ../

default: Out.txt Test.txt Benchmark.svg

Out.txt: $(addprefix $(BPATH), main.exe)
	$^ > $@

Test.txt: $(addprefix $(BPATH), test.exe)
	$^ > $@
	
Benchmark.svg: $(addprefix $(MPATH), Benchmark.gpi) $(addprefix $(DPATH), Benchmark.data)
	gnuplot $<