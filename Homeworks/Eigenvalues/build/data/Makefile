BPATH = ../build/
TIME = time --portability --append --output $@

MAKEFLAGS += -j8

MYVAR = 8



ParallelisationComparison.txt: 
	$(TIME) $(MAKE) CreateData
	$(RM) -rf *.data
	@echo ------------------------- >> $@
	$(TIME) $(MAKE) CreateDataParallel




Time.txt: CreateData CreateDataParallel
	@echo Time for creating data in a for loop >> $@
	$(TIME) $(MAKE) CreateData
	$(RM) -rf *.data
	@echo ------------------------- >> $@
	@echo Time for creating data in a for loop parallel $@		
	$(TIME) $(MAKE) $(MAKEFLAGS) CreateDataParallel


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

CreateDataParallel: $(addprefix $(BPATH), hydrogenswave.exe)
	@echo Doing forloop, please be patient..
	for number in $$(seq 1 $(MYVAR)); \
		do \
		  	mono $^ -rmax:10 -dr:0.$$number > "Hydrogen_dr=0."$$number".data" &\
		done; \
		wait 
		

CreateData: $(addprefix $(BPATH), hydrogenswave.exe)
	@echo Doing forloop, please be patient..
	for number in $$(seq 1 $(MYVAR)); \
		do \
		  	mono $^ -rmax:10 -dr:0.$$number > "Hydrogen_dr=0."$$number".data"; \
		done;
