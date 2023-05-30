package com.jorgebg.classprobuilder.student

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.jorgebg.classprobuilder.R
import com.jorgebg.classprobuilder.databases.Student_Subject_Provisional
import com.jorgebg.classprobuilder.databases.SubjectProvisional

class recyclerAdapterSubjectStud(val subject : List<Student_Subject_Provisional>, val view: student_signature ) : RecyclerView.Adapter<recyclerAdapterSubjectStud.viewHolder>() {



    class viewHolder(val view: View) :RecyclerView.ViewHolder(view){

        val nombre : TextView = view.findViewById(R.id.name_selected)


    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): viewHolder {

        val layoutInflater = LayoutInflater.from(parent.context)
        return viewHolder(layoutInflater.inflate(R.layout.recycler_name, parent, false))
    }

    override fun onBindViewHolder(holder: viewHolder, position: Int) {

        with(subject[position]){
            holder.nombre.text = subject[position].nombre

            holder.itemView.setOnClickListener{
                view.showDialog(subject[position].nombre, subject[position].id,subject[position].subjectId,subject[position].studentId)
            }
        }

    }

    override fun getItemCount(): Int {
       return subject.size
    }
}