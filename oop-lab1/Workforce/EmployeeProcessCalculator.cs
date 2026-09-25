using oop_lab1.SKU;

namespace oop_lab1.Workforce;

public class EmployeeProcessCalculator
{
    private readonly List<Employee> _employees;

    public EmployeeProcessCalculator(List<Employee> employees)
    {
        _employees = employees;
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
            if (!SkuAllocator.TryLoadEmployee(employee, remainingSkus))
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
                if (!SkuAllocator.TryLoadEmployee(employee, remainingSkus))
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

    private void ProcessEmployeesMoving(TimeSpan elapsedTime,
        List<(Employee Employee, TimeSpan RemainingMoveTime)> movingEmployees,
        Queue<Employee> employeeQueue)
    {
        for (var i = movingEmployees.Count - 1; i >= 0; i--)
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