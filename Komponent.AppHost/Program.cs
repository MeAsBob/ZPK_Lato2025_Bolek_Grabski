using k8s.KubeConfigModels;
using TodoAppv2.Components;

private readonly TaskManagerComponent _taskManager;
private readonly TaskQueryComponent _taskQuery;

_taskManager = new TaskManagerComponent(_context);
_taskQuery = new TaskQueryComponent();


Tasks = await _taskQuery.GetFilteredTasksAsync(_context);
await _taskManager.AddTaskAsync(NewTask);
