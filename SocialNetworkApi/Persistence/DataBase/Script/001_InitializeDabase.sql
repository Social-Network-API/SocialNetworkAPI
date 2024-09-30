CREATE TABLE Users (
    user_id UUID PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    password VARCHAR(255) NOT NULL,
    profile_image VARCHAR(255)
);

CREATE TABLE Posts (
    post_id UUID PRIMARY KEY,
    user_id UUID REFERENCES Users(user_id) ON DELETE CASCADE,
    content TEXT NOT NULL,
    image VARCHAR(255),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE Comments (
    comment_id UUID PRIMARY KEY,
    post_id UUID REFERENCES Posts(post_id) ON DELETE CASCADE,
    user_id UUID REFERENCES Users(user_id) ON DELETE CASCADE,
    content TEXT NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE Likes (
    like_id UUID PRIMARY KEY,
    post_id UUID REFERENCES Posts(post_id) ON DELETE CASCADE,
    user_id UUID REFERENCES Users(user_id) ON DELETE CASCADE
);
