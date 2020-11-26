public void MyTaskAsync(string[] files)
{
  MyTaskWorkerDelegate worker = new MyTaskWorkerDelegate(MyTaskWorker);
  AsyncCallback completedCallback = new AsyncCallback(MyTaskCompletedCallback);

  // Fehlerkorrektur
  int i = 56;


  // Hotfix im DPL System
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