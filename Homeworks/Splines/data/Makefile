BPATH = ../build/

default: data

data: $(addprefix $(BPATH), main.exe)
	mono $(filter %.exe, $^)