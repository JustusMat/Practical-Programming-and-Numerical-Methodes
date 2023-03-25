BPATH = ../build/
OPATH = ../output/

default: example.data OWF.data LotkaVolterra.data ThreeBodyProblem.data
	
example.data: $(addprefix $(BPATH), example.exe)
	$^ > $@

OWF.data: $(addprefix $(BPATH), OWF.exe)
	$^ > $@
	
LotkaVolterra.data: $(addprefix $(BPATH), LotkaVolterra.exe)
	$^ > $@
	
ThreeBodyProblem.data: $(addprefix $(BPATH), ThreeBodyProblem.exe)
	$^ > $@