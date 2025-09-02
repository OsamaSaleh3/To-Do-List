let taskInput=document.getElementById("taskInput");
let addTaskBtn=document.querySelector(".addTaskBtn");
let taskList =document.querySelector(".taskList");

addTaskBtn.addEventListener("click",()=>{
    const text = taskInput.value.trim();
    if(text==="")return;
    let task=document.createElement("li");
    task.className="task";

    let checkTask=document.createElement("input");
    checkTask.type="checkbox";
    let taskDescription=document.createElement("p");
    taskDescription.textContent=text;

    checkTask.addEventListener("change",()=>{
        if(checkTask.checked){
            taskDescription.style.textDecoration="line-through";
        }
        else{
            taskDescription.style.textDecoration="none";
        }
    });

    task.appendChild(checkTask);
    task.appendChild(taskDescription);
    taskList.appendChild(task);

    taskInput.value = "";
  taskInput.focus();
});