BPATH = ./build/
SPATH = ./source/
DPATH = ./data/
OPATH = ./output/



default:
	cd $(BPATH) && $(MAKE)
	cd $(DPATH) && $(MAKE)
	cd $(OPATH) && $(MAKE)

.PHONY: build test 
build:
	@echo "== Building build=="
	cd $(BPATH) &&  $(MAKE)

test:
	@echo "== Building test.txt"
	cd $(BPATH); mono main.exe > ../out.txt


.PHONY:clean 
clean:
	$(RM) - rf $(addprefix $(BPATH), *.exe *.dll)
	$(RM) - rf $(addprefix $(DPATH), *.data *.txt)
	$(RM) - rf $(addprefix $(OPATH), *.svg *.log *.txt)
	clear
	@echo all build, data and output files cleared