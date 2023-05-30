package com.jorgebg.classprobuilder.student

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.jorgebg.classprobuilder.R
import com.jorgebg.classprobuilder.databases.Student_ponderation_provisional
import com.jorgebg.classprobuilder.databases.ponderacion
import com.jorgebg.classprobuilder.databases.subject_ponderation_provisional

class recyclerAdapterMarkStudent(val marks : List<Student_ponderation_provisional>, val view: student_marks ) : RecyclerView.Adapter<recyclerAdapterMarkStudent.viewHolder>() {



    class viewHolder(val view: View) : RecyclerView.ViewHolder(view){

        val nombre : TextView = view.findViewById(R.id.name_selected)
        val mark : TextView = view.findViewById(R.id.mark)
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): viewHolder {
        val layoutInflater = LayoutInflater.from(parent.context)
        return viewHolder(layoutInflater.inflate(R.layout.recycler_student_marks, parent, false))
    }

    override fun onBindViewHolder(holder: viewHolder, position: Int) {
        with(marks[position]){
            holder.nombre.text = marks[position].name
            holder.mark.text = marks[position].nota.toString()

            holder.itemView.setOnClickListener{
                view.showDialog(marks[position].name, marks[position].id)
            }
        }
    }

    override fun getItemCount(): Int {
        return marks.size
    }

}