SPATH = ./codepieces/
BPATH = ./build/
DPATH = ./data/
OPATH = ./output/

default: 
	cd $(BPATH) && $(MAKE)	
	cd $(DPATH) && $(MAKE) 
	cd $(OPATH) && $(MAKE)


build:
	cd $(BPATH) && $(MAKE)

plot:
	$(RM) -rf $(addprefix $(OPATH), *.exe *.dll *.txt *.svg *.data)
	cd $(OPATH) && $(MAKE)

#################################################################################
clean:
	$(RM) -rf $(addprefix $(SPATH), *.exe *.dll *.txt *.svg *.data)
	$(RM) -rf $(addprefix $(BPATH), *.exe *.dll *.txt *.svg *.data)
	$(RM) -rf $(addprefix $(DPATH), *.exe *.dll *.txt *.svg *.data)
	$(RM) -rf $(addprefix $(OPATH), *.exe *.dll *.txt *.svg *.data)
	clear
	@echo cleaned