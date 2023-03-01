SPATH = ./codepieces/
OPATH = ./output/
DPATH = ./data/



default: 

################################################################################
Gamma.svg: gamma.gpi $(addprefix $(DPATH), gamma.data)
	gnuplot $<

LnGamma.svg: lngamma.gpi $(addprefix $(DPATH), lngamma.data)
	gnuplot $<




#################################################################################
gamma.data: $(addprefix $(OPATH), evaluategammafunction.exe)
	mono $< > $> $(addprefix $(DPATH), $@) 

lngamma.data: $(addprefix $(OPATH), evaluatelngammafunction.exe)
	mono $< > $> $(addprefix $(DPATH), $@) 




#################################################################################
evaluategammafunction.exe: $(addprefix $(SPATH), evaluategammafunction.cs) $(addprefix $(OPATH), sfuncs.dll)
	mcs -target:exe -out:$(addprefix $(OPATH),$@) \
		$(filter %.cs, $^)	\
		$(addprefix -reference:,$(filter %.dll,$^))
		
evaluatelngammafunction.exe: $(addprefix $(SPATH), evaluatelngammafunction.cs) $(addprefix $(OPATH), sfuncs.dll)
	mcs -target:exe -out:$(addprefix $(OPATH),$@) \
		$(filter %.cs, $^)	\
		$(addprefix -reference:,$(filter %.dll,$^))
		




#################################################################################
sfuncs.dll: $(addprefix $(SPATH), erf.cs gamma.cs lngamma.cs)
	mcs -target:library -out:$(addprefix $(OPATH),$@) $^


#################################################################################
clean:
	$(RM) -rf $(addprefix $(OPATH), *.exe *.dll *.txt *.svg)
	$(RM) -rf $(addprefix $(DPATH), *.data)
	clear