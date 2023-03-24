BPATH = ../build/
OPATH = ../output/

default: example.data OWF.data
	
example.data: $(addprefix $(BPATH), example.exe)
	$^> $@

OWF.data: $(addprefix $(BPATH), OWF.exe)
	$^> $@