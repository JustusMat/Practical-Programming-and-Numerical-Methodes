DPATH = ../data/
MPATH = ../
default: animation.gif TBPanimation.gif

animation.gif: $(addprefix $(MPATH), animation.gpi) $(addprefix $(DPATH), LotkaVolterra.data)
	gnuplot $<
	
TBPanimation.gif: $(addprefix $(MPATH), TBPanimation.gpi) $(addprefix $(DPATH), ThreeBodyProblem.data)
	gnuplot $<