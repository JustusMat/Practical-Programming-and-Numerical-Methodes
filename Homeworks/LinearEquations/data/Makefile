BPATH = ../build/


default: Benchmark.data

Benchmark.data: $(addprefix $(BPATH), Benchmark.exe)
	@echo Please be patient...
	$^ > $@
	@echo Finished \:\)

