TIME = time --portability --append --output $@

SPATH = ./codepieces/
BPATH = ./Outputs/

N=5e8

#Commands to use:
#make --> all:
#make clean
#make Output/xyz.file-extension

all: $(BPATH)Out.txt $(BPATH)main.exe $(BPATH)pfor.exe $(BPATH)multiprocessing.dll $(BPATH)inbuildmultiprocessing.dll


$(BPATH)Out.txt: $(BPATH)main.exe $(BPATH)pfor.exe
	>$@
	#$(TIME) mono $(BPATH)main.exe -terms:$(N) -threads:2 >> $(BPATH)Out.txt
	#$(TIME) mono $(BPATH)pfor.exe -terms:$(N) -threads:2 >> $(BPATH)Out.txt
	#$(TIME) mono $(BPATH)main.exe -terms:$(N) -threads:3 >> $(BPATH)Out.txt
	#$(TIME) mono $(BPATH)pfor.exe -terms:$(N) -threads:3 >> $(BPATH)Out.txt
	for number in 1 2 3 4; do \
  		$(TIME) mono $(BPATH)main.exe -terms:$(N) -threads:$$number >> $(BPATH)Out.txt; \
  		$(TIME) mono $(BPATH)pfor.exe -terms:$(N) -threads:$$number >> $(BPATH)Out.txt; \
	done


	

$(BPATH)main.exe: $(SPATH)main.cs $(BPATH)multiprocessing.dll 
	mcs -target:exe	-out:$@ \
		$(addprefix -reference:,$(filter %.dll, $^)) \
		$(filter %.cs,$^)
		
$(BPATH)pfor.exe: $(SPATH)pfor.cs $(BPATH)inbuildmultiprocessing.dll
	mcs -target:exe -out:$@ \
		$(addprefix -reference:,$(filter %.dll, $^)) \
		$(filter %.cs, $^)


$(BPATH)multiprocessing.dll: $(SPATH)multiprocessing.cs
	mcs -target:library -out:$@ $^

$(BPATH)inbuildmultiprocessing.dll: $(SPATH)inbuildmultiprocessing.cs
	mcs -target:library -out:$@ $^
	
clean:
	$(RM) $(BPATH)*.exe $(BPATH)*.dll $(BPATH)*.txt