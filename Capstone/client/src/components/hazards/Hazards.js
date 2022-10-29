import React from "react";
import { useNavigate } from "react-router-dom";


export const Hazards = ({ hazard }) => {
    return (

        <section className="hazard-details">
            <p className="Name panel-item">
                <span className="title">{hazard?.name}
                    {/* {isApproved ? <button className="btn" onClick={(() => navigate(`/ingredient/edit/${ingredient.id}`))}><i className="fa-solid fa-eye-dropper"></i></button> : ""}

                {isAdmin ? <button onClick={(() => navigate(`/ingredient/delete/${ingredient.id}`))} className="btn"><i class="fa-solid fa-trash-can"></i></button> : ""} */}
                </span>
                {hazard?.description}

            </p>
        </section>
    )
}