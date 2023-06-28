BPATH = ../build/
DPATH = ../data/
MPATH = ../

default: Interpolation.svg Integration.svg Derivative.svg CubicComparison.svg


Interpolation.svg: $(addprefix $(MPATH), Interpolation.gpi) $(addprefix $(DPATH), GraphData.data interpolationdata.data) 
	gnuplot $<

Integration.svg: $(addprefix $(MPATH), Integration.gpi) $(addprefix $(DPATH), integratedinterpolated.data interpolationdata.data) 
	gnuplot $<

Derivative.svg: $(addprefix $(MPATH), Derivative.gpi) $(addprefix $(DPATH), derivativedata.data)
	gnuplot $<
	
CubicComparison.svg: $(addprefix $(MPATH), CubicComparison.gpi) $(addprefix $(DPATH), integratedinterpolated.data interpolationdata.data)
	gnuplot $<