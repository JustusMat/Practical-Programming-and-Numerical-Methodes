BPATH = ../build/

default: gamma.data lngamma.data
	@echo data success!
	

#################################################################################
gamma.data: $(addprefix $(BPATH), evaluategammafunction.exe)
	mono $< > $> $@ 

lngamma.data: $(addprefix $(BPATH), evaluatelngammafunction.exe)
	mono $< > $> $@ 
	
