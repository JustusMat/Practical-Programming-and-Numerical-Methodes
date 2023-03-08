BPATH = ../build/

default: CreateData

datafileExample.data: $(addprefix $(BPATH), hydrogenswave.exe)
	mono $^ \
		-rmax:10 \
		-dr:1.0 \
		> $@

#datafiles:
#	for number in $$(seq 0 $(MYVAR)); \
#	do \
#		echo "test".$$number; \
#	done

MYVAR = 8
CreateData: $(addprefix $(BPATH), hydrogenswave.exe)
	@echo Doing forloop, please be patient..
	for number in $$(seq 1 $(MYVAR)); \
		do \
		  	mono $^ -rmax:10 -dr:0.$$number > "Hydrogen_dr=0."$$number".data"; \
		done