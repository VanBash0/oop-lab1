namespace oop_lab1;

public class EmployeeManager
{
    private readonly List<Employee> _employees;
    
    public EmployeeManager(IEnumerable<Employee> employees)
    {
        _employees = employees.ToList();
        _employees.Sort();
    }

    public TimeSpan CalculateLoadTime(Manifest manifest)
    {
        var remainingSkus = manifest.SkuSets.ToList();
        var employeesAtTruck = new Queue<Employee>(_employees);
        var movingEmployees = new List<(Employee Employee, TimeSpan RemainingMoveTime)>();
        var totalTime = TimeSpan.Zero;

        while (remainingSkus.Count > 0)
        {
            if (employeesAtTruck.Count == 0)
            {
                var shortestMoveTime = movingEmployees.Min(employee => employee.RemainingMoveTime);
                totalTime += shortestMoveTime;
                ProcessEmployeesMoving(shortestMoveTime, movingEmployees, employeesAtTruck);
                continue;
            }

            var employee = employeesAtTruck.Dequeue();
            if (!TryLoadEmployee(employee, remainingSkus))
            {
                continue;
            }

            totalTime += employee.LoadTime;
            ProcessEmployeesMoving(employee.LoadTime, movingEmployees, employeesAtTruck);
            
            movingEmployees.Add((employee, employee.MoveTime));
        }

        return totalTime;
    }

    public TimeSpan CalculateUnloadTime(Manifest manifest)
    {
        var remainingSkus = manifest.SkuSets.ToList();
        var totalTime = TimeSpan.Zero;
        var employeesAtTruck = new Queue<Employee>();
        var employeesAtWarehouse = new Queue<Employee>(_employees);
        var movingEmployees = new List<(Employee Employee, TimeSpan RemainingMoveTime)>();

        while (remainingSkus.Count > 0 || employeesAtTruck.Count > 0 || movingEmployees.Count > 0)
        {
            while (employeesAtWarehouse.Count > 0)
            {
                var employee = employeesAtWarehouse.Dequeue();
                if (!TryLoadEmployee(employee, remainingSkus))
                {
                    continue;
                }
                movingEmployees.Add((employee, employee.MoveTime));
            }

            if (employeesAtTruck.Count == 0)
            {
                var shortestMoveTime = movingEmployees.Min(employee => employee.RemainingMoveTime);
                totalTime += shortestMoveTime;
                ProcessEmployeesMoving(shortestMoveTime, movingEmployees, employeesAtTruck);
            }
            
            var unloadingEmployee = employeesAtTruck.Dequeue();
            totalTime += unloadingEmployee.LoadTime;
            ProcessEmployeesMoving(unloadingEmployee.LoadTime, movingEmployees, employeesAtTruck);
            employeesAtWarehouse.Enqueue(unloadingEmployee);
        }
        
        return totalTime;
    }
    
    private bool TryLoadEmployee(Employee employee, List<SkuSet> remainingSkus)
    {
        var freeCapacity = employee.Capacity;
        var isLoaded = false;
        
        int index = 0;
        while (index < remainingSkus.Count && freeCapacity > 0)
        {
            var skuSet = remainingSkus[index];
            var fittingCount = (uint)Math.Min(Math.Floor(freeCapacity / skuSet.Sku.VolumeWeightChars), skuSet.Quantity);
                
            if (fittingCount == 0)
            {
                index++;
                continue;
            }

            freeCapacity -= skuSet.Sku.VolumeWeightChars * fittingCount;
            isLoaded = true;
            if (fittingCount == skuSet.Quantity)
            {
                remainingSkus.RemoveAt(index);
            }
            else
            {
                remainingSkus[index] = skuSet with { Quantity = skuSet.Quantity - fittingCount };
                index++;
            }
        }
        
        return isLoaded;
    }

    private void ProcessEmployeesMoving(TimeSpan elapsedTime,
        List<(Employee Employee, TimeSpan RemainingMoveTime)> movingEmployees,
        Queue<Employee> employeeQueue)
    {
        for (int i = 0; i < movingEmployees.Count; i++)
        {
            var movingEmployee = movingEmployees[i];
            var remainingMoveTime = movingEmployee.RemainingMoveTime - elapsedTime;

            if (remainingMoveTime <= TimeSpan.Zero)
            {
                employeeQueue.Enqueue(movingEmployee.Employee);
                movingEmployees.RemoveAt(i);
                continue;
            }

            movingEmployees[i] = (movingEmployee.Employee, remainingMoveTime);
        }
    }
}
