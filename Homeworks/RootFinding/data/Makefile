BPATH = ../build/

default: BSHydrogen.data
	
BSHydrogen.data: $(addprefix $(BPATH), BSHydrogen.exe)
	mono $^ > $@