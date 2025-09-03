const API_BASE="https://localhost:7130/api/TodoItems";

let taskInput=document.getElementById("taskInput");
let addTaskBtn=document.querySelector(".addTaskBtn");
let taskList =document.querySelector(".taskList");
const loadBtn = document.getElementById("loadBtn");

addTaskBtn.addEventListener("click",()=>{
    const text = taskInput.value.trim();
    if(text==="")return;
    let task=document.createElement("li");
    task.className="task";

    let checkTask=document.createElement("input");
    checkTask.type="checkbox";
    let taskDescription=document.createElement("p");
    taskDescription.textContent=text;
    let deleteBtn=document.createElement("button");
    deleteBtn.textContent="delete";
    deleteBtn.className="deleteBtn";

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
    task.appendChild(deleteBtn);
    taskList.appendChild(task);

    taskInput.value = "";
  taskInput.focus();
});

async function loadTodos(){
    taskList.innerHTML = "<li>...جاري التحميل</li>";
    try{
        const res=await fetch(API_BASE,{
            method:"GET",
        });
        if(!res.ok){
            throw new Error(`HTTP ${res.status}`);
        }
        const data=await res.json();
        taskList.innerHTML="";
         data.forEach(item => {
        const text = item.title;
    if(text==="")return;
    let task=document.createElement("li");
    task.className="task";

    let checkTask=document.createElement("input");
    checkTask.type="checkbox";
    checkTask.checked=(item.isCompleted);
    let taskDescription=document.createElement("p");
    taskDescription.textContent=text;
    let deleteBtn=document.createElement("button");
    deleteBtn.textContent="delete";
    deleteBtn.className="deleteBtn";
    if(item.isCompleted){
        taskDescription.style.textDecoration="line-through";
    }
    else{
        taskDescription.style.textDecoration="none";
    }

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
    task.appendChild(deleteBtn);
    taskList.appendChild(task);
        });
        if (data.length === 0) {
      taskList.innerHTML = "<li>لا توجد مهام بعد</li>";
    }
    }
    catch(err){
        console.error(err);
    taskList.innerHTML = "<li style='color:#c00'>فشل تحميل المهام (افتح تبويب Network)</li>";
    alert("فشل تحميل المهام من الخادم");
}
}
loadBtn.addEventListener("click", () => {
  loadTodos();
});