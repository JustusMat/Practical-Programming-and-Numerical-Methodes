BPATH = ../build/
DPATH = ../data/
MPATH = ../

default: Interpolation.svg Derivative.svg Antiderivative.svg
	@echo Finished output

Interpolation.svg: $(addprefix $(MPATH), FunctionInterpolationPlot.gpi) $(addprefix $(DPATH), PlotData.data PlotDataAkima.data Interpolation.data) 
	gnuplot $<

Derivative.svg: $(addprefix $(MPATH), FunctionDerivativePlot.gpi) $(addprefix $(DPATH), PlotDerivativeData.data PlotDerivativeDataAkima.data)
	gnuplot $<

Antiderivative.svg: $(addprefix $(MPATH), FunctionAntiderivativePlot.gpi) $(addprefix $(DPATH), PlotAntiderivativeData.data PlotAntiderivativeDataAkima.data) 
	gnuplot $<

	
