BPATH = ../build/

default: data
	@echo Finished data

data: $(addprefix $(BPATH), main.exe)
	mono $(filter %.exe, $^)