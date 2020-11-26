public void MyTaskAsync(string[] files)
{
  MyTaskWorkerDelegate worker = new MyTaskWorkerDelegate(MyTaskWorker);
  AsyncCallback completedCallback = new AsyncCallback(MyTaskCompletedCallback);

  lock (_sync)
  {
    if (_myTaskIsRunning)
      throw new InvalidOperationException("The control is currently busy.");

    AsyncOperation async = AsyncOperationManager.CreateOperation(null);
    worker.BeginInvoke(files, completedCallback, async);
    _myTaskIsRunning = true;
  }
}

private readonly object _sync = new object();