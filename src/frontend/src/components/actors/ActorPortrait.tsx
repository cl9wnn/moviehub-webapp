import React from "react";

type ActorPortraitProps = {
  photoUrl?: string;
  firstName?: string;
  lastName?: string;
};

const ActorPortrait: React.FC<ActorPortraitProps> = ({ photoUrl, firstName, lastName }) => (
  <div className="w-full lg:w-1/3 max-w-[250px] ml-10">
    <img
      src={`http://${photoUrl}`}
      alt={firstName + " " + lastName}
      className="w-full rounded-xl object-cover max-h-[600px]"
    />
  </div>
);

export default ActorPortrait;