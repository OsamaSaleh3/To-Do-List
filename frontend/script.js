const API_BASE="https://localhost:7130/api/TodoItems";

let taskInput=document.getElementById("taskInput");
let addTaskBtn=document.querySelector(".addTaskBtn");
let taskList =document.querySelector(".taskList");
const loadBtn = document.getElementById("loadBtn");

async function addTodo(text){
    try{
         const res = await fetch(API_BASE, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                title: text
            })
        });
        if(!res.ok){
            throw new Error(`POST /TodoItems -> ${res.status}`);
        }
        const data=await res.json();
        return data;
    }
    catch(error){
         console.error("Error adding todo:", error);
        throw error;
    }
}

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
            task.dataset.id = String(item.id); 

            let checkTask=document.createElement("input");
            checkTask.type="checkbox";
            checkTask.checked = !!item.isCompleted;

            let taskDescription=document.createElement("p");
            taskDescription.textContent=text;

            let deleteBtn=document.createElement("button");
            deleteBtn.textContent="delete";
            deleteBtn.className="deleteBtn";

            if(item.isCompleted){
                taskDescription.style.textDecoration="line-through";
            } else {
                taskDescription.style.textDecoration="none";
            }

            checkTask.addEventListener("change", async ()=>{
                taskDescription.style.textDecoration = checkTask.checked ? "line-through" : "none";

                try {
                    const res = await fetch(`${API_BASE}`, { 
                        method: "PUT",
                        headers: { "Content-Type": "application/json" },
                        body: JSON.stringify({
                            id: Number(task.dataset.id),
                            title: text,
                            isCompleted: checkTask.checked 
                        })
                    });
                    if(!res.ok){
                        throw new Error(`Error updating the task : ${res.status}`);
                    }
                } catch (error) {
                    console.error(`error with changing the status of the task: ${error}`);
                }
            });

            deleteBtn.addEventListener("click", async () => {
                const id = task.dataset.id;
                if (!id || id === "undefined") { 
                    await loadTodos(); 
                    return;
                }
                const backup = task.cloneNode(true);
                const nextSibling = task.nextSibling;
                task.remove();

                const ok = await deleteTodo(id);
                if (!ok) {
                    taskList.insertBefore(backup, nextSibling);
                    alert("فشل حذف المهمة");
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

async function deleteTodo(id){
    try{
            res = await fetch(`${API_BASE}?id=${id}`, { method:"DELETE" });        
        if(!res.ok){
            throw new Error(`Failed to delete (status ${res.status})`);
        }
        return true;
    }
    catch(err){
         console.error("Error deleting todo:", err);
        return false;
    }
}

addTaskBtn.addEventListener("click", ()=>{
    const text = taskInput.value.trim();
    if(text==="")return;

    addTodo(text)
      .then(() => loadTodos())
      .catch(() => {
        alert("فشل إضافة المهمة");
      });

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
        taskDescription.style.textDecoration = checkTask.checked ? "line-through" : "none";
    });

    deleteBtn.addEventListener("click", async () => {
        if (!task.dataset.id) {
            await loadTodos(); 
            return;
        }
        const id = task.dataset.id;
        const backup = task.cloneNode(true);
        const nextSibling = task.nextSibling;
        task.remove();
        const ok = await deleteTodo(id);
        if (!ok) {
            taskList.insertBefore(backup, nextSibling);
            alert("فشل حذف المهمة");
        }
    });

    task.appendChild(checkTask);
    task.appendChild(taskDescription);
    task.appendChild(deleteBtn);
    taskList.appendChild(task);

    taskInput.value = "";
    taskInput.focus();
});

loadBtn.addEventListener("click", () => {
  loadTodos();
});
