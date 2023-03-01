SPATH = ../codepieces/

default: 	evaluategammafunction.exe evaluatelngammafunction.exe
	@echo build success!

#################################################################################
evaluategammafunction.exe: $(addprefix $(SPATH), evaluategammafunction.cs) sfuncs.dll
	mcs -target:exe -out:$@ \
		$(filter %.cs, $^)	\
		$(addprefix -reference:,$(filter %.dll,$^))
		
evaluatelngammafunction.exe: $(addprefix $(SPATH), evaluatelngammafunction.cs) sfuncs.dll
	mcs -target:exe -out:$@ \
		$(filter %.cs, $^)	\
		$(addprefix -reference:,$(filter %.dll,$^))
		

#################################################################################
sfuncs.dll: $(addprefix $(SPATH), erf.cs gamma.cs lngamma.cs)
	mcs -target:library -out:$@ $^
		
