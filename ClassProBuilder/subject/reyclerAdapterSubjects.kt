package com.jorgebg.classprobuilder.subject

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.jorgebg.classprobuilder.R
import com.jorgebg.classprobuilder.databases.SubjectProvisional
import com.jorgebg.classprobuilder.student.recyclerAdapterStudents

class reyclerAdapterSubjects(val subjects : List<SubjectProvisional>, val view: subject_menu) : RecyclerView.Adapter<reyclerAdapterSubjects.viewHolder>() {



    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): viewHolder {

        val layoutInflater = LayoutInflater.from(parent.context)
        return viewHolder(layoutInflater.inflate(R.layout.recycler_name, parent, false))
    }

    override fun onBindViewHolder(holder: viewHolder, position: Int) {

        with(subjects[position]){
            holder.nombre.text = subjects[position].nombre

            holder.itemView.setOnClickListener{
                view.showDialog(subjects[position].nombre, subjects[position].id)
            }
        }
    }

    override fun getItemCount(): Int {

        return subjects.size
    }

    class viewHolder(val view: View) :RecyclerView.ViewHolder(view){

        val nombre : TextView = view.findViewById(R.id.name_selected)

    }
}