BPATH = ../build/
TIME = time --portability --append --output $@

MAKEFLAGS += -j8

MYVAR = 8


default: ParallelisationComparison.txt Variation_dr.data Variation_rmax.data hydrogenwaves.data


ParallelisationComparison.txt: 
	@echo Non parallel times >> $@
	$(TIME) $(MAKE) CreateData
	$(RM) -rf Hydrogen_dr*
	@echo ------------------------- >> $@
	@echo Parallel times >> $@
	$(TIME) $(MAKE) CreateDataParallel
	$(RM) -rf *Hydrogen_dr*


Time.txt: CreateData CreateDataParallel
	@echo Time for creating data in a for loop >> $@
	$(TIME) $(MAKE) CreateData
	$(RM) -rf *.data
	@echo ------------------------- >> $@
	@echo Time for creating data in a for loop parallel $@		
	$(TIME) $(MAKE) $(MAKEFLAGS) CreateDataParallel


datafileExample.data: $(addprefix $(BPATH), hydrogendiagonalisation.exe)
	mono $^ \
		-rmax:10 \
		-dr:1.0 \
		> $@

#datafiles:
#	for number in $$(seq 0 $(MYVAR)); \
#	do \
#		echo "test".$$number; \
#	done

CreateDataParallel: $(addprefix $(BPATH), hydrogendiagonalisation.exe)
	@echo Doing forloop, please be patient..
	for number in $$(seq 1 $(MYVAR)); \
		do \
		  	mono $^ -rmax:10 -dr:0.$$number > "Hydrogen_dr=0."$$number".data" &\
		done; \
		wait 
		

Variation_dr.data: $(addprefix $(BPATH), hydrogendiagonalisation.exe) 
	for number in $$(seq 1 $(MYVAR)); \
	do \
	  	mono $^ -rmax:10 -dr:0.$$number >> $@ &\
	  	done; \
	  	wait 

Variation_rmax.data: $(addprefix $(BPATH), hydrogendiagonalisation.exe)
	for number in $$(seq 1 $(MYVAR));\
	do \
	  mono $^ -rmax:$$number -dr:0.25 >> $@ &\
	  done;\
	  wait 

hydrogenwaves.data: $(addprefix $(BPATH), hydrogenwavefunction.exe)
	mono $^ >> $@;




CreateData: $(addprefix $(BPATH), hydrogendiagonalisation.exe)
	@echo Doing forloop, please be patient..
	for number in $$(seq 1 $(MYVAR)); \
		do \
		  	mono $^ -rmax:10 -dr:0.$$number > "Hydrogen_dr=0."$$number".data"; \
		done;
