public class hydrogenhamiltonianclass
{
    public double Rmax;
    public double DeltaR;
    public int Npoints;
    public matrix Hamiltonian;
    public vector r;
    
    public hydrogenhamiltonianclass(double rmax, double deltaR)
    {
        Rmax = rmax;
        DeltaR = deltaR;
        Npoints = (int)(rmax/deltaR)-1;
        Hamiltonian = HamiltonianConstruction();
    }

    matrix HamiltonianConstruction()
    {
        Hamiltonian = new matrix(Npoints, Npoints);
        r = new vector(Npoints);
        for (int i = 0; i < Npoints; i++)
        {
            r[i] = DeltaR * (i + 1);
        }

        for (int i = 0; i < Npoints - 1; i++)
        {
            Hamiltonian.set(i,i,-2);
            Hamiltonian.set(i,i+1,1);
            Hamiltonian.set(i+1,i,1);
        }
        Hamiltonian.set(Npoints-1,Npoints-1,-2);
        Hamiltonian *= -0.5 / DeltaR / DeltaR;
        for (int i = 0; i < Npoints; i++)
        {
            Hamiltonian[i, i] += -1 / r[i];
        }

        return Hamiltonian;
    }
}